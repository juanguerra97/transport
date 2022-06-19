import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {BodegaDto, BodegasClient} from "../../web-api-client";
import {getErrorMessage} from "../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";
import {FiltroPedido} from "../../models/filtro-pedido";
import {DatePipe} from "@angular/common";
import {DateTime} from "luxon";

@Component({
  selector: 'app-filtro-pedidos',
  templateUrl: './filtro-pedidos.component.html',
  styleUrls: ['./filtro-pedidos.component.scss']
})
export class FiltroPedidosComponent implements OnInit {

  @Input() filtrando = false;
  @Output() onFiltrar = new EventEmitter<FiltroPedido>();
  @Output() onLimpiar = new EventEmitter<FiltroPedido>();

  form = new FormGroup({
    bodegaId: new FormControl(null, []),
    descripcionMaterial: new FormControl(null, []),
    fechaDel: new FormControl(null, []),
    fechaAl: new FormControl(null, [])
  });

  bodegas: BodegaDto[] = [];

  filtros: FiltroPedido = {};

  constructor(
    private bodegasClient: BodegasClient,
    private toastService: HotToastService,
    private date: DatePipe,
  ) {
  }

  async ngOnInit() {
    this.bodegas = await this.cargarBodegas();
    this.filtros = this.getFiltrosFromStorage();
    this.form.patchValue(this.filtros);
    this.filtrar();
  }

  private cargarBodegas() {
    return new Promise<BodegaDto[]>(((resolve, reject) => {
      this.bodegasClient.getAllBodegas()
        .subscribe({
          next: res => {
            resolve(res);
          },
          error: error => {
            console.error(error);
            this.toastService.error(getErrorMessage(error), {
              dismissible: true,
              duration: 15000,
              position: 'bottom-center'
            });
            resolve([]);
          }
        });
    }));
  }

  filtrar() {
    this.filtros = this.buildFiltro();
    this.saveFiltrosToStorage(this.filtros);
    this.onFiltrar.emit(this.filtros);
  }

  limpiar() {
    this.filtros = {};
    this.form.reset(this.filtros);
    this.saveFiltrosToStorage(this.filtros);
    this.onLimpiar.emit(this.filtros);
  }

  private buildFiltro() {
    const val = this.form.value;
    const filtros: FiltroPedido = {bodegaId: val.bodegaId, descripcionMaterial: val.descripcionMaterial};
    filtros.fechaDel = this.date.transform(val.fechaDel, 'dd/MM/yyyy') as any;
    filtros.fechaAl = this.date.transform(val.fechaAl, 'dd/MM/yyyy') as any;
    return filtros;
  }

  private getFiltrosFromStorage(): FiltroPedido {
    try {
      const filtroStr = localStorage.getItem('transport.pedidos.admin.filtro');
      if (filtroStr) {
        const filtroObj = JSON.parse(filtroStr);
        if (filtroObj.fechaDel) {
          filtroObj.fechaDel = DateTime.fromFormat(filtroObj.fechaDel, 'dd/MM/yyyy').toJSDate();
        }
        if (filtroObj.fechaAl) {
          filtroObj.fechaAl = DateTime.fromFormat(filtroObj.fechaAl, 'dd/MM/yyyy').toJSDate();
        }
        return filtroObj;
      }
    }catch(error) {
      console.error(error);
    }
    return {};
  }

  private saveFiltrosToStorage(filtros: FiltroPedido) {
    try {
      localStorage.setItem('transport.pedidos.admin.filtro', JSON.stringify(filtros));
    } catch(error) {
      console.error(error);
    }
  }

}
