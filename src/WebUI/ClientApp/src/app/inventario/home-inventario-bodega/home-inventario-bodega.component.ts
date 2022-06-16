import {Component, OnInit} from '@angular/core';
import {
  BodegaDto,
  BodegasClient, InventarioClient,
  IPaginatedListOfInventarioBodegaDto,
} from "../../web-api-client";
import {getErrorMessage} from "../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";
import {ActivatedRoute} from "@angular/router";
import {DialogService} from "primeng/dynamicdialog";
import {NewIngresoMaterialComponent} from "../new-ingreso-material/new-ingreso-material.component";

@Component({
  selector: 'app-home-inventario-bodega',
  templateUrl: './home-inventario-bodega.component.html',
  styleUrls: ['./home-inventario-bodega.component.scss']
})
export class HomeInventarioBodegaComponent implements OnInit {

  TIPOS_BODEGA_LABEL: any = {
    0: 'BODEGA',
    1: 'PLANTA'
  };

  bodega?: BodegaDto;
  loadingBodega = true;

  inventario: IPaginatedListOfInventarioBodegaDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingInventario = true;

  modalNuevoIngresoAbierto = false;

  descripcionMaterial: string | undefined = undefined;

  constructor(
    private bodegasClient: BodegasClient,
    private inventarioClient: InventarioClient,
    private dialogService: DialogService,
    private toastService: HotToastService,
    private ar: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.ar.paramMap.subscribe({
      next: params => {
        const id = Number.parseInt(params.get('id') as any);
        this.cargarBodega(id);
      }
    });
  }

  private cargarBodega(bodegaId: any) {
    this.loadingBodega = true;
    this.bodegasClient.getBodegaById(bodegaId)
      .subscribe({
        next: res => {
          this.bodega = res;
          this.loadingBodega = false;
          this.cargarInventario();
        },
        error: error => {
          console.error(error);
          this.loadingBodega = false;
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  private cargarInventario(pageSize: number = 10, pageNumber: number = 1, descripcionMaterial: string | undefined = undefined) {
    this.loadingInventario = true;
    this.inventarioClient.getInventarioByBodega(this.bodega?.id as any, pageSize, pageNumber, descripcionMaterial)
      .subscribe({
        next: res => {
          this.inventario = res;
          this.loadingInventario = false;
        },
        error: error => {
          console.error(error);
          this.loadingInventario = false;
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
    this.cargarInventario(pageSize, pageNumber, this.getDescripcionMaterial());
  }

  private getDescripcionMaterial() {
    return this.descripcionMaterial || undefined;
  }

  filtrar() {
    this.cargarInventario(this.inventario?.pageSize, this.inventario?.pageNumber, this.getDescripcionMaterial());
  }

  openModalNuevo() {
    if (this.modalNuevoIngresoAbierto) {
      return;
    }
    this.modalNuevoIngresoAbierto = true;
    const ref = this.dialogService.open(NewIngresoMaterialComponent, {
      header: 'Ingreso de Material',
      styleClass: 'modal-catalogo',
      data: this.bodega
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalNuevoIngresoAbierto = false;
        if (res) {
          this.cargarInventario(this.inventario?.pageSize, this.inventario?.pageNumber, this.getDescripcionMaterial());
        }
      }
    });
  }

}
