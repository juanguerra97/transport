import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {PedidoMaterialDto, PedidosClient} from "../../web-api-client";
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

  constructor(
    private pedidosClient: PedidosClient,
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

  isPendiente() {
    return this.pedido?.estadoPedidoMaterial?.id === EstadosPedido.PENDIENTE_ID;
  }

}
