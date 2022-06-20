import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {EstadosConductor} from "../../../utils/constants";
import {ConductorDto, ConductoresClient, IPaginatedListOfConductorDto} from "../../../web-api-client";
import {ConfirmationService} from "primeng/api";
import {DialogService} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";
import {NewConductorComponent} from "../new-conductor/new-conductor.component";
import {EditConductorComponent} from "../edit-conductor/edit-conductor.component";

@Component({
  selector: 'app-home-conductores',
  templateUrl: './home-conductores.component.html',
  styleUrls: ['./home-conductores.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeConductoresComponent implements OnInit {

  ESTADOS_LABEL: any = {
    [EstadosConductor.ACTIVO]: 'ALTA',
    [EstadosConductor.INACTIVO]: 'BAJA'
  };

  conductores: IPaginatedListOfConductorDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingConductores = true;

  modalNuevoAbierto = false;
  modalEditarAbierto = false;

  nombre?: string = undefined;

  constructor(
    private conductoresClient: ConductoresClient,
    private confirmationService: ConfirmationService,
    private dialogService: DialogService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.cargarConductores();
  }

  private cargarConductores(pageSize: number = 10, pageNumber: number = 1, nombre: string | undefined = undefined) {
    this.loadingConductores = true;
    this.cdr.markForCheck();
    this.conductoresClient.getConductores(pageSize, pageNumber, nombre)
      .subscribe({
        next: res => {
          this.conductores = res;
          this.loadingConductores = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingConductores = false;
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
    this.cargarConductores(pageSize, pageNumber, this.getNombre());
  }

  getNombre() {
    return this.nombre || undefined;
  }

  confirmarEliminar(conductor: ConductorDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres eliminar el conductor seleccionado?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(conductor);
      }
    });
  }

  eliminar(conductor: ConductorDto) {
    this.conductoresClient.delete(conductor.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó un conductor.`, {position: 'bottom-center'});
          this.cargarConductores(this.conductores?.pageSize, this.conductores.pageNumber, this.getNombre());
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
    const ref = this.dialogService.open(NewConductorComponent, {
      header: 'Nuevo',
      styleClass: 'modal-catalogo'
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalNuevoAbierto = false;
        if (res) {
          this.cargarConductores(this.conductores?.pageSize, this.conductores?.pageNumber, this.getNombre());
        }
      }
    });
  }

  openModalEditar(conductor: ConductorDto) {
    if (this.modalEditarAbierto) {
      return;
    }
    this.modalEditarAbierto = true;
    const ref = this.dialogService.open(EditConductorComponent, {
      header: 'Editar',
      styleClass: 'modal-catalogo',
      data: conductor
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalEditarAbierto = false;
        this.cargarConductores(this.conductores?.pageSize, this.conductores?.pageNumber, this.getNombre());
      }
    });
  }

  filtrar() {
    this.cargarConductores(this.conductores?.pageSize, this.conductores?.pageNumber, this.getNombre());
  }

  isEstadoAlta(conductor: ConductorDto) {
    return conductor.status === EstadosConductor.ACTIVO;
  }

  isEstadoBaja(conductor: ConductorDto) {
    return conductor.status === EstadosConductor.INACTIVO;
  }

  confirmarBaja(conductor: ConductorDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres dar de baja el conductor seleccionado?`,
      acceptLabel: 'Sí, dar de baja',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.darDeBaja(conductor);
      }
    });
  }

  darDeBaja(conductor: ConductorDto) {
    this.conductoresClient.inactivar(conductor.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se dió de baja un conductor.`, {position: 'bottom-center'});
          this.cargarConductores(this.conductores?.pageSize, this.conductores.pageNumber, this.getNombre());
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

  confirmarAlta(conductor: ConductorDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres dar de alta el conductor seleccionado?`,
      acceptLabel: 'Sí, dar de alta',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.darDeAlta(conductor);
      }
    });
  }

  darDeAlta(conductor: ConductorDto) {
    this.conductoresClient.activar(conductor.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se dió de alta un vehiculo.`, {position: 'bottom-center'});
          this.cargarConductores(this.conductores?.pageSize, this.conductores.pageNumber, this.getNombre());
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
