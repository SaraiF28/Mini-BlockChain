# Mini-Blockchain  
Alumna:Sarai Fregozo Aréchiga  
Código:219523217  

---

## Descripción general

Este proyecto es una simulación básica del funcionamiento de una blockchain.  
Cada bloque contiene un hash generado a partir de su información y del hash anterior.  
Si un bloque es alterado manualmente, la cadena pierde integridad y el sistema identifica la modificación.

El proyecto fue desarrollado en **C#**, usando **Windows Forms** y **MySQL** como sistema de almacenamiento.

---

## Características principales

- Cadena de bloques enlazados mediante hashes.
- Cálculo automático de hash para cada bloque.
- Verificación de integridad: detecta bloques manipulados.
- Persistencia en MySQL.
- Interfaz gráfica en Windows Forms que permite:
  - Agregar nuevos bloques.
  - Visualizar todos los bloques.
  - Verificar la integridad completa de la cadena.

---

## Estructura del proyecto

### `Block.cs`
Define el modelo de un bloque. Contiene:
- `id`
- `timestamp`
- `data`
- `prev_hash`
- `current_hash`

### `Blockchain.cs`
Incluye la lógica principal:
- Creación de nuevos bloques.
- Enlace con el hash del bloque anterior.
- Generación de hashes.
- Inserción y carga desde MySQL.
- Verificación de integridad de toda la cadena.

### `Form1.cs`
Interfaz gráfica que permite:
- Agregar datos.
- Ver bloques.
- Ejecutar la verificación de la cadena.

### `conexionDB.cs`
Clase encargada de devolver la conexión MySQL al resto del proyecto.

---

## Tecnologías utilizadas

- C#
- Windows Forms
- .NET Framework **4.7.2**
- MySQL Server
- MySQL Workbench
- MySQL Connector/NET

---

## Configuración de la base de datos

Ejecutar la siguiente tabla en MySQL:

```sql
CREATE TABLE blocks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    timestamp VARCHAR(30) NOT NULL,
    data TEXT NOT NULL,
    prev_hash CHAR(64) NOT NULL,
    current_hash CHAR(64) NOT NULL
);

