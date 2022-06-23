import {PrimeNGConfig} from 'primeng/api';

export class IdiomaEs {

  static idiomaEs(config: PrimeNGConfig): PrimeNGConfig {
    config.setTranslation({

      startsWith: 'Comienza con',
      contains: 'Contiene',
      notContains: 'No contiene',
      endsWith: 'Termina con',
      equals: 'Igual',
      notEquals: 'No es igual',
      noFilter: 'Sin filtro',
      lt: 'Menos que',
      lte: 'Menos que o igual a',
      gt: 'Mas grande que',
      gte: 'Mayor qué o igual a',
      is: 'Es',
      isNot: 'No es',
      before: 'Antes',
      after: 'Después',
      clear: 'Limpiar',
      apply: 'Filtrar',
      matchAll: 'Coincidir con todos',
      matchAny: 'Coincidir con cualquiera',
      addRule: 'Agregar regla',
      removeRule: 'Eliminar regla',
      accept: 'Si',
      reject: 'No',
      choose: 'Escoger',
      upload: 'Subir',
      cancel: 'Cancelar',
      dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
      dayNamesShort: ['DOM', 'LUN', 'MAR', 'MIE', 'JUE', 'VIE', 'SAB'],
      dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
      monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
      monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'Mayo', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
      today: 'Hoy',
      weekHeader: 'Semana',
      dateIs: 'Igual',
      dateIsNot: 'Distinta',
      dateAfter: 'Después de',
      dateBefore: 'Antes de',
      emptyMessage: 'Sin resultados',
      emptyFilterMessage: 'Sin resultados'
    });

    return config;
  }

}
