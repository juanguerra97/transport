

export abstract class Paises {
  static GUATEMALA_ID = 1;
}

export abstract class EstadosPedido {
  static CREADO_ID = 1;
  static PENDIENTE_ID = 2;
  static APROBADO_ID = 3;
  static PROGRAMADO_ID = 4;
  static COMPLETADO_ID = 5;
  static ANULADO_ID = 6;
}

export abstract class EstadosVehiculo {
  static ACTIVO = 'A';
  static INACTIVO = 'I';
  static ELIMINADO = 'X';
}
