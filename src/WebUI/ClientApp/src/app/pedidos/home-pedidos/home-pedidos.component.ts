import {Component, OnInit} from '@angular/core';
import {IPaginatedListOfPedidoMaterialDto, PedidosClient} from "../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../utils/errors";
import {FiltroPedido} from "../../models/filtro-pedido";

@Component({
  selector: 'app-home-pedidos',
  templateUrl: './home-pedidos.component.html',
  styleUrls: ['./home-pedidos.component.scss']
})
export class HomePedidosComponent implements OnInit {

  pedidos: IPaginatedListOfPedidoMaterialDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingPedidos = true;

  filtros: FiltroPedido = {};

  constructor(
    private pedidosClient: PedidosClient,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {

  }

  private cargarPedidos(pageSize: number = 10, pageNumber: number = 1, bodegaId: number | undefined = undefined, descripcionMaterial: string | undefined = undefined, fechaDel: string | undefined = undefined, fechaAl: string | undefined = undefined) {
    this.loadingPedidos = true;
    this.pedidosClient.getPedidos(pageSize, pageNumber, bodegaId, descripcionMaterial, fechaDel, fechaAl)
      .subscribe({
        next: res => {
          this.pedidos = res;
          this.loadingPedidos = false;
        },
        error: error => {
          console.error(error);
          this.loadingPedidos = false;
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  paginar($event: any) {
    const pageNumber = ($event.page || 0) + 1;
    const pageSize = $event.rows;
    this.cargarPedidos(pageSize, pageNumber, this.filtros.bodegaId, this.filtros.descripcionMaterial, this.filtros.fechaDel, this.filtros.fechaAl);
  }

  filtrar(filtros: FiltroPedido) {
    this.filtros = filtros;
    this.cargarPedidos(this.pedidos.pageSize, 1, filtros.bodegaId, filtros.descripcionMaterial, filtros.fechaDel, filtros.fechaAl);
  }

}
