import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MovimientoBodegaClient, MovimientoBodegaDto, PedidoMaterialDto, PedidosClient} from "../../web-api-client";
import {getErrorMessage} from "../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";
import {EstadosPedido} from "../../utils/constants";

@Component({
  selector: 'app-administracion-pedido',
  templateUrl: './administracion-pedido.component.html',
  styleUrls: ['./administracion-pedido.component.scss']
})
export class AdministracionPedidoComponent implements OnInit {

  pedido?: PedidoMaterialDto;
  loadingPedido = true;

  aprobando = false;

  movimientos: MovimientoBodegaDto[] = [];
  loadingMovimientos = true;

  constructor(
    private pedidosClient: PedidosClient,
    private movimientoBodegaClient: MovimientoBodegaClient,
    private toastService: HotToastService,
    private ar: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.ar.paramMap.subscribe({
      next: params => {
        const pedidoId = Number.parseInt(params.get('pedidoId') as any);
        this.cargarPedido(pedidoId);
      }
    });
  }

  private cargarPedido(pedidoId: any) {
    this.loadingPedido = true;
    this.pedidosClient.getPedidoById(pedidoId)
      .subscribe({
        next: res => {
          this.pedido = res;
          this.loadingPedido = false;
          this.cargarMovimientos(pedidoId);
        },
        error: error => {
          console.error(error);
          this.loadingPedido = false;
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  private cargarMovimientos(pedidoId: any) {
    this.loadingMovimientos = true;
    this.movimientoBodegaClient.getMovimientosBodegaByPedido(pedidoId)
      .subscribe({
        next: res => {
          this.movimientos = res;
          this.loadingMovimientos = false;
        },
        error: error => {
          console.error(error);
          this.loadingMovimientos = false;
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  isPendiente() {
    return this.pedido?.estadoPedidoMaterial?.id === EstadosPedido.PENDIENTE_ID;
  }

  aprobar() {
    this.aprobando = true;
    this.pedidosClient.aprobarPedido(this.pedido?.id as any)
      .subscribe({
        next: res => {
          this.pedido = res;
          this.aprobando = false;
          this.toastService.success('El pedido fue aprobado con exito.', { position: 'bottom-center'});
        },
        error: error => {
          console.error(error);
          this.aprobando = false;
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

}
