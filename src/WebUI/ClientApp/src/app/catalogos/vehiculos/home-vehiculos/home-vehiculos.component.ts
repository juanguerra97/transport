import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {IPaginatedListOfProveedorDto, VehiculoDto, VehiculosClient} from "../../../web-api-client";
import {ConfirmationService} from "primeng/api";
import {DialogService} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";
import {EstadosVehiculo} from "../../../utils/constants";
import {NewVehiculoComponent} from "../new-vehiculo/new-vehiculo.component";
import {EditVehiculoComponent} from "../edit-vehiculo/edit-vehiculo.component";

@Component({
  selector: 'app-home-vehiculos',
  templateUrl: './home-vehiculos.component.html',
  styleUrls: ['./home-vehiculos.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeVehiculosComponent implements OnInit {

  ESTADOS_LABEL: any = {
    [EstadosVehiculo.ACTIVO]: 'ALTA',
    [EstadosVehiculo.INACTIVO]: 'BAJA'
  };

  vehiculos: IPaginatedListOfProveedorDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingVehiculos = true;

  modalNuevoAbierto = false;
  modalEditarAbierto = false;

  descripcion?: string = undefined;

  constructor(
    private vehiculosClient: VehiculosClient,
    private confirmationService: ConfirmationService,
    private dialogService: DialogService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) { }

  ngOnInit(): void {
    this.cargarVehiculos();
  }

  private cargarVehiculos(pageSize: number = 10, pageNumber: number = 1, descripcion: string | undefined = undefined, codigo: string | undefined = undefined, placa: string | undefined = undefined, status: string | undefined = undefined)
  {
    this.loadingVehiculos = true;
    this.cdr.markForCheck();
    this.vehiculosClient.getVehiculos(pageSize, pageNumber, descripcion, codigo, placa, status)
      .subscribe({
        next: res => {
          this.vehiculos = res;
          this.loadingVehiculos = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingVehiculos = false;
          this.cdr.markForCheck();
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
    this.cargarVehiculos(pageSize, pageNumber, this.getDescripcion());
  }

  getDescripcion() {
    return this.descripcion || undefined;
  }

  confirmarEliminar(vehiculo: VehiculoDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres eliminar el vehiculo seleccionado?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(vehiculo);
      }
    });
  }

  eliminar(vehiculo: VehiculoDto) {
    this.vehiculosClient.delete(vehiculo.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó un vehiculo.`, {position: 'bottom-center'});
          this.cargarVehiculos(this.vehiculos?.pageSize, this.vehiculos.pageNumber, this.getDescripcion());
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

  openModalNuevo() {
    if (this.modalNuevoAbierto) {
      return;
    }
    this.modalNuevoAbierto = true;
    const ref = this.dialogService.open(NewVehiculoComponent, {
      header: 'Nuevo',
      styleClass: 'modal-catalogo'
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalNuevoAbierto = false;
        if (res) {
          this.cargarVehiculos(this.vehiculos?.pageSize, this.vehiculos?.pageNumber, this.getDescripcion());
        }
      }
    });
  }

  openModalEditar(vehiculo: VehiculoDto) {
    if (this.modalEditarAbierto) {
      return;
    }
    this.modalEditarAbierto = true;
    const ref = this.dialogService.open(EditVehiculoComponent, {
      header: 'Editar',
      styleClass: 'modal-catalogo',
      data: vehiculo
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalEditarAbierto = false;
        this.cargarVehiculos(this.vehiculos?.pageSize, this.vehiculos?.pageNumber, this.getDescripcion());
      }
    });
  }

  filtrar() {
    this.cargarVehiculos(this.vehiculos?.pageSize, this.vehiculos?.pageNumber, this.getDescripcion());
  }

  isEstadoAlta(vehiculo: VehiculoDto) {
    return vehiculo.status === EstadosVehiculo.ACTIVO;
  }

  isEstadoBaja(vehiculo: VehiculoDto) {
    return vehiculo.status === EstadosVehiculo.INACTIVO;
  }

  confirmarBaja(vehiculo: VehiculoDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres dar de baja el vehiculo seleccionado?`,
      acceptLabel: 'Sí, dar de baja',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.darDeBaja(vehiculo);
      }
    });
  }

  darDeBaja(vehiculo: VehiculoDto) {
    this.vehiculosClient.inactivar(vehiculo.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se dió de baja un vehiculo.`, {position: 'bottom-center'});
          this.cargarVehiculos(this.vehiculos?.pageSize, this.vehiculos.pageNumber, this.getDescripcion());
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

  confirmarAlta(vehiculo: VehiculoDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres dar de alta el vehiculo seleccionado?`,
      acceptLabel: 'Sí, dar de alta',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.darDeAlta(vehiculo);
      }
    });
  }

  darDeAlta(vehiculo: VehiculoDto) {
    this.vehiculosClient.activar(vehiculo.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se dió de alta un vehiculo.`, {position: 'bottom-center'});
          this.cargarVehiculos(this.vehiculos?.pageSize, this.vehiculos.pageNumber, this.getDescripcion());
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
