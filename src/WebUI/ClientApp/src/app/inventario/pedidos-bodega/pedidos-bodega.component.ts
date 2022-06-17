import {Component, Input, OnInit} from '@angular/core';
import {
  BodegaDto,
  IPaginatedListOfPedidoMaterialDto,
  PedidoMaterialDto,
  PedidosClient
} from "../../web-api-client";
import {DialogService} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../utils/errors";
import {NewPedidoMaterialComponent} from "../new-pedido-material/new-pedido-material.component";
import {ConfirmationService} from "primeng/api";
import {EditPedidoMaterialComponent} from "../edit-pedido-material/edit-pedido-material.component";
import {EstadosPedido} from "../../utils/constants";

@Component({
  selector: 'app-pedidos-bodega',
  templateUrl: './pedidos-bodega.component.html',
  styleUrls: ['./pedidos-bodega.component.scss']
})
export class PedidosBodegaComponent implements OnInit {

  @Input() bodega?: BodegaDto;

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

  modalNuevoPedidoAbierto = false;
  modalEditarPedidoAbierto = false;

  constructor(
    private pedidosClient: PedidosClient,
    private confirmationService: ConfirmationService,
    private dialogService: DialogService,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {
    this.cargarPedidos();
  }

  private cargarPedidos(pageSize: number = 10, pageNumber: number = 1, descripcionMaterial: string | undefined = undefined, fechaDel: string | undefined = undefined, fechaAl: string | undefined = undefined) {
    this.loadingPedidos = true;
    this.pedidosClient.getPedidosByBodega(this.bodega?.id as any, pageSize, pageNumber, descripcionMaterial, fechaDel, fechaAl)
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
    this.cargarPedidos(pageSize, pageNumber);
  }

  openModalNuevoPedido() {
    if (this.modalNuevoPedidoAbierto) {
      return;
    }
    this.modalNuevoPedidoAbierto = true;
    const ref = this.dialogService.open(NewPedidoMaterialComponent, {
      header: 'Nuevo Pedido',
      styleClass: 'modal-catalogo',
      data: this.bodega
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalNuevoPedidoAbierto = false;
        if (res) {
          this.cargarPedidos(this.pedidos?.pageSize, this.pedidos?.pageNumber);
        }
      }
    });
  }

  openModalEditarPedido(pedido: PedidoMaterialDto) {
    if (this.modalEditarPedidoAbierto) {
      return;
    }
    this.modalEditarPedidoAbierto = true;
    const ref = this.dialogService.open(EditPedidoMaterialComponent, {
      header: 'Modificar Pedido',
      styleClass: 'modal-catalogo',
      data: pedido
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalEditarPedidoAbierto = false;
        if (res) {
          this.cargarPedidos(this.pedidos?.pageSize, this.pedidos?.pageNumber);
        }
      }
    });
  }

  confirmarEnviar(pedido: PedidoMaterialDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres enviar el pedido seleccionado? Una vez enviado, ya no podrá ser modificado.`,
      acceptLabel: 'Sí, enviar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.enviar(pedido);
      }
    });
  }

  enviar(pedido: PedidoMaterialDto) {
    this.pedidosClient.enviarPedido(pedido.id as any)
      .subscribe({
        next: res => {
          this.cargarPedidos(this.pedidos?.pageSize, this.pedidos?.pageNumber);
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  confirmarAnular(pedido: PedidoMaterialDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres anular el pedido seleccionado?`,
      acceptLabel: 'Sí, anular',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.anular(pedido);
      }
    });
  }

  anular(pedido: PedidoMaterialDto) {
    this.pedidosClient.anularPedido(pedido.id as any)
      .subscribe({
        next: res => {
          this.cargarPedidos(this.pedidos?.pageSize, this.pedidos?.pageNumber);
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  puedeEnviar(pedido: PedidoMaterialDto) {
    return pedido.estadoPedidoMaterial?.id === EstadosPedido.CREADO_ID;
  }

  puedeEditar(pedido: PedidoMaterialDto) {
    return pedido.estadoPedidoMaterial?.id === EstadosPedido.CREADO_ID;
  }

  puedeAnular(pedido: PedidoMaterialDto) {
    return pedido.estadoPedidoMaterial?.id === EstadosPedido.CREADO_ID
      || pedido.estadoPedidoMaterial?.id === EstadosPedido.PENDIENTE_ID;
  }

}
