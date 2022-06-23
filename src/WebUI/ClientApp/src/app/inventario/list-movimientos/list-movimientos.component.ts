import {ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit} from '@angular/core';
import {
  BodegaDto,
  IPaginatedListOfMovimientoBodegaDto,
  MovimientoBodegaClient,
  MovimientoBodegaDto
} from "../../web-api-client";
import {ConfirmationService} from "primeng/api";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../utils/errors";
import {EstadosMovimiento} from "../../utils/constants";

@Component({
  selector: 'app-list-movimientos',
  templateUrl: './list-movimientos.component.html',
  styleUrls: ['./list-movimientos.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListMovimientosComponent implements OnInit {

  @Input() bodega?: BodegaDto;

  movimientos: IPaginatedListOfMovimientoBodegaDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingMovimientos = true;

  constructor(
    private movimientoBodegaClient: MovimientoBodegaClient,
    private confirmationService: ConfirmationService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) { }

  ngOnInit(): void {
    this.cargarMovimientos(this.bodega?.id);
  }

  private cargarMovimientos(bodegaDestinoId: any, pageSize: number = 10, pageNumber = 1, descripcionMaterial: string | undefined = undefined, bodegaOrigenId: number | undefined = undefined, conductorId: number | undefined = undefined, vehiculoId: number | undefined = undefined) {
    this.loadingMovimientos = true;
    this.cdr.markForCheck();
    this.movimientoBodegaClient.getMovimientosByBodegaDestino(bodegaDestinoId, pageSize, pageNumber, descripcionMaterial, bodegaOrigenId, conductorId, vehiculoId)
      .subscribe({
        next: res => {
          this.movimientos = res;
          this.loadingMovimientos = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingMovimientos = false;
          this.cdr.markForCheck();
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 10000,
            position: 'bottom-center'
          });
        }
      });
  }

  paginar($event: any) {
    const pageNumber = ($event.page || 0) + 1;
    const pageSize = $event.rows;
    this.cargarMovimientos(this.bodega?.id, pageSize, pageNumber);
  }

  isCargado(movimiento: MovimientoBodegaDto) {
    return movimiento.estadoMovimientoBodega?.id === EstadosMovimiento.CARGADO_ID;
  }

  confirmarEntregar(movimiento: MovimientoBodegaDto) {
    this.confirmationService.confirm({
      key: 'confirmListMovimientos',
      message: `¿Seguro que quieres marcar la entrega como ENTREGADA?`,
      acceptLabel: 'Sí, completar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.entregar(movimiento);
      },
    });
  }

  private entregar(movimiento: MovimientoBodegaDto) {
    this.movimientoBodegaClient.entregarMovimiento(movimiento.id as any)
      .subscribe({
        next: res => {
          movimiento.estadoMovimientoBodega = res.estadoMovimientoBodega;
          this.cdr.markForCheck();
          this.toastService.success(`La entrega se marco como entregada.`, {position: 'bottom-center'});
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

}
