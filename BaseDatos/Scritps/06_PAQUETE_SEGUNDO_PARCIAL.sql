-- Ingresar al esquema PROVEEDOR_LEADS_4
ALTER SESSION SET CURRENT_SCHEMA = PROVEEDOR_LEADS_4;

-- En el paquete "SegundoParcial" en Oracle
CREATE OR REPLACE PACKAGE SegundoParcial AS
  -- Procedimiento para insertar una factura y obtener su número
  PROCEDURE InsertarFactura(
    p_Fecha IN DATE,
    p_IdCliente IN NUMBER,
    p_NumeroFactura OUT NUMBER,
    p_Estado OUT VARCHAR2,
    p_DescripcionError OUT VARCHAR2
  );
  
  -- Procedimiento para insertar una venta (detalle)
  PROCEDURE InsertarVenta(
    p_IdFactura IN NUMBER,
    p_IdProducto IN NUMBER,
    p_Cantidad IN NUMBER,
    p_Estado OUT VARCHAR2,
    p_DescripcionError OUT VARCHAR2
  );

END SegundoParcial;
/

-- Dentro del cuerpo del paquete "SegundoParcial" en Oracle
CREATE OR REPLACE PACKAGE BODY SegundoParcial AS
  -- Procedimiento para insertar una factura y obtener su número
  PROCEDURE InsertarFactura(
    p_Fecha IN DATE,
    p_IdCliente IN NUMBER,
    p_NumeroFactura OUT NUMBER,
    p_Estado OUT VARCHAR2,
    p_DescripcionError OUT VARCHAR2
  ) AS
  BEGIN
    -- Inicializa los parámetros de salida
    p_NumeroFactura := NULL;
    p_Estado := 'EXITO';
    p_DescripcionError := NULL;

    -- Genera el número de factura usando una secuencia
    SELECT FACTURA_SEQ.NEXTVAL INTO p_NumeroFactura FROM DUAL;

    -- Inserta la factura en la tabla FACTURAS
    INSERT INTO FACTURAS (ID_FACTURA, FECHA, ID_CLIENTE)
    VALUES (p_NumeroFactura, p_Fecha, p_IdCliente);
  EXCEPTION
    WHEN OTHERS THEN
      -- En caso de error, actualiza el estado y la descripción del error
      p_Estado := 'ERROR';
      p_DescripcionError := 'BD: ' || SQLERRM;
  END InsertarFactura;

  -- Procedimiento para insertar una venta (detalle)
  PROCEDURE InsertarVenta(
    p_IdFactura IN NUMBER,
    p_IdProducto IN NUMBER,
    p_Cantidad IN NUMBER,
    p_Estado OUT VARCHAR2,
    p_DescripcionError OUT VARCHAR2
  ) AS
  BEGIN
    -- Inicializa los parámetros de salida
    p_Estado := 'EXITO';
    p_DescripcionError := NULL;

    -- Inserta la venta en la tabla VENTAS
    INSERT INTO VENTAS (ID_VENTA, ID_FACTURA, ID_PRODUCTO, CANTIDAD)
    VALUES (VENTA_SEQ.NEXTVAL, p_IdFactura, p_IdProducto, p_Cantidad);
  EXCEPTION
    WHEN OTHERS THEN
      -- En caso de error, actualiza el estado y la descripción del error
      p_Estado := 'ERROR';
      p_DescripcionError := 'BD: ' || SQLERRM;
  END InsertarVenta;

  -- Resto de los procedimientos (ConfirmarTransaccion) aquí...
END SegundoParcial;
/