DROP DATABASE IF EXISTS miniblock;
CREATE DATABASE miniblock;
USE miniblock;

CREATE TABLE blocks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    timestamp VARCHAR(30) NOT NULL,   -- mantiene milisegundos EXACTOS
    data TEXT NOT NULL,
    prev_hash CHAR(64) NOT NULL,
    current_hash CHAR(64) NOT NULL
);

UPDATE blocks 
SET 
    data = 'HACKEADO',
    current_hash = 'AAAAA11111BBBBB22222CCCC33333',
    prev_hash = 'ZZZZZ99999YYYYY88888XXXXX77777'
WHERE id = 3;

UPDATE blocks 
SET 
    data = 'HACKEADO',
    current_hash = 'FFF111EEE222DDD333CCC444BBB555',
    prev_hash = 'PPPP9999OOOO8888NNNN7777MMMM6666'
WHERE id = 5;

UPDATE blocks 
SET 
    data = 'HACKEADO',
    current_hash = 'AAA666BBB777CCC888DDD999EEE000',
    prev_hash = 'FFFF1111EEEE2222DDDD3333CCCC4444'
WHERE id = 6;

SELECT * FROM blocks;


SELECT * FROM blocks;


USE miniblock;
-- Desactiva la seguridad para borrar sin WHERE
SET SQL_SAFE_UPDATES = 0;

-- Borra todos los bloques
TRUNCATE TABLE blocks;

-- Vuelve a activar safe 
SET SQL_SAFE_UPDATES = 1;