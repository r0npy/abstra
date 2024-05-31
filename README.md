# Configuración del Entorno de Desarrollo con HTTPS

Para configurar un entorno de desarrollo local seguro, es necesario instalar y configurar OpenSSL para generar certificados SSL/TLS válidos en localhost. Estos pasos también incluyen la instalación de Chocolatey, un gestor de paquetes para Windows, que facilitará la instalación de OpenSSL.

## Paso 1: Instalar Chocolatey

Chocolatey te permite instalar herramientas y aplicaciones de manera rápida desde la línea de comandos en Windows.

1. **Abrir PowerShell/Terminal como administrador**: 

2. **Ejecutar el comando de instalación**:
   ```sh
   Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
   ```
   
## Paso 2: Instalar OpenSSL con Chocolatey

Una vez instalado Chocolatey, el siguiente paso es instalar OpenSSL, una herramienta esencial para la creación y gestión de certificados SSL/TLS.

### Instalar OpenSSL

Para instalar OpenSSL usando Chocolatey, sigue estos pasos:

1. **Ejecutar el comando de instalación de Chocolatey para OpenSSL**:
   ```sh
   choco install openssl  
   ```

2. **Verificar instalacion de  OpenSSL**:
   ```sh
   openssl version
   ```
## Paso 3: Generar Certificados SSL/TLS

Una vez instalado OpenSSL, puedes proceder a generar un certificado SSL para `localhost`, que será utilizado para asegurar las conexiones a tus aplicaciones de desarrollo local. Este proceso también implica el uso de **mkcert**, una herramienta que simplifica la creación de certificados de desarrollo local que son automáticamente confiables.

### Instalar mkcert

Antes de generar los certificados, necesitas instalar **mkcert**:

1. **Abrir PowerShell como administrador** y ejecutar:
   ```sh
   choco install mkcert
   ```

2. **Instalar almacen de certificados localhost** y ejecutar:
   ```sh
   mkcert -install
   ```

3. **Generar el Certificado para localhost** y ejecutar:
   ```sh
   mkcert localhost 127.0.0.1 ::1 192.168.0.102
   ```

> Nota: acá podes agregar las IPs que usan tu PC, como este ejemplo para que todas sean validas.

Esto creará dos archivos: uno para el certificado (localhost+2.pem) y otro para la clave privada (localhost+2-key.pem).

3. **Convertir Certificados a Formato PFX** y ejecutar:
   ```sh
   openssl pkcs12 -export -out localhost.pfx -inkey localhost+2-key.pem -in localhost+2.pem
   ```

En este punto solicitará ingresar la clave privada de los certificados (en el caso de los proyectos que usen los PFX se ingresarán en el proyecto posteriormente)
## Paso 4: Configurar el Proyecto .NET

Después de generar y preparar tu certificado SSL/TLS, necesitas configurar tu proyecto .NET para utilizar este certificado, asegurando así una conexión HTTPS segura para el desarrollo local.

### Mover el Certificado al Proyecto

Para asegurar que el entorno de desarrollo esté organizado y que la aplicación .NET pueda acceder fácilmente a los certificados necesarios, sigue estos pasos:

1. **Mover el archivo `localhost.pfx` a la carpeta `Keys` dentro de tu proyecto .NET**:
   - Si no existe una carpeta `Keys`, créala en la raíz de tu proyecto.
   - Copia el archivo `localhost.pfx` a esta carpeta. Esta localización ayudará a mantener organizado tu entorno de desarrollo.

### Configurar Kestrel para Usar HTTPS

.NET utiliza Kestrel como servidor web por defecto en aplicaciones web. Para configurar Kestrel para usar el certificado SSL, modifica el archivo de configuración `appsettings.json` o `appsettings.Development.json` si quieres aplicar esta configuración solo en el entorno de desarrollo.

```json
{
  "Kestrel": {
    "Endpoints": {
      "HTTPS": {
        "Url": "https://localhost:7000",
        "Certificate": {
          "Path": "Keys/localhost.pfx",
          "Password": "tu_contraseña_del_pfx"
        }
      }
    }
  }
}
```

# Para ejecutar en ubuntu/linux sin necesidad de tener una sesion SSH abierta

Instalar `screen` para poder ejecutar sin sesiones dependiente de SSH o PuTTY

   ```sh
   sudo apt-get update
   sudo apt-get install screen
   ```

Abrir una sesion nueva desde SSH
   ```sh
   screen
   ```

Lanzar la aplicacion con la configuración de kestrel
   ```sh
   dotnet rhsk.api.firmatech.dll &
   ```

> Nota: el simbolo `&` al final es necesarios para poder cortar el terminal con `ctrl + c` y seguir con otros comandos

# Detener el servicio para varios fines

Ejecutar

   ```sh
   ps ax | grep dotnet
   ```

Este comando devolverá el `PID` que se podrá luego `matar` con:

   ```sh
   kill {PID}
   ```