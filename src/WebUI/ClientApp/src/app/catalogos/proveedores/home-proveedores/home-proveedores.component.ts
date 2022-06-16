import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {IPaginatedListOfProveedorDto, ProveedorDto, ProveedoresClient} from "../../../web-api-client";
import {ConfirmationService} from "primeng/api";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";
import {DialogService} from "primeng/dynamicdialog";
import {NewProveedorComponent} from "../new-proveedor/new-proveedor.component";
import {EditProveedorComponent} from "../edit-proveedor/edit-proveedor.component";

@Component({
  selector: 'app-home-proveedores',
  templateUrl: './home-proveedores.component.html',
  styleUrls: ['./home-proveedores.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeProveedoresComponent implements OnInit {

  proveedores: IPaginatedListOfProveedorDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingProveedores = true;

  modalNuevoAbierto = false;
  modalEditarAbierto = false;

  nombre: string|undefined;

  constructor(
    private proveedoresClient: ProveedoresClient,
    private confirmationService: ConfirmationService,
    private dialogService: DialogService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.cargarProveedores();
  }

  private cargarProveedores(pageSize: number = 10, pageNumber: number = 1, nombre: string|undefined = undefined, nit: string|undefined = undefined, telefono: string|undefined = undefined, email: string|undefined = undefined) {
    this.loadingProveedores = true;
    this.cdr.markForCheck();
    this.proveedoresClient.getProveedores(pageSize, pageNumber, nombre, nit, telefono, email)
      .subscribe({
        next: res => {
          this.proveedores = res;
          this.loadingProveedores = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingProveedores = false;
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
    this.cargarProveedores(pageSize, pageNumber, this.getNombre());
  }

  confirmarEliminar(proveedor: ProveedorDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres eliminar el proveedor seleccionado?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(proveedor);
      }
    });
  }

  eliminar(proveedor: ProveedorDto) {
    this.proveedoresClient.delete(proveedor.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó un proveedor.`, {position: 'bottom-center'});
          this.cargarProveedores(this.proveedores?.pageSize, this.proveedores.pageNumber, this.getNombre());
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
    const ref = this.dialogService.open(NewProveedorComponent, {
      header: 'Nuevo',
      styleClass: 'modal-catalogo'
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalNuevoAbierto = false;
        if (res) {
          this.cargarProveedores(this.proveedores?.pageSize, this.proveedores?.pageNumber, this.getNombre());
        }
      }
    });
  }

  openModalEditar(proveedor: ProveedorDto) {
    if (this.modalEditarAbierto) {
      return;
    }
    this.modalEditarAbierto = true;
    const ref = this.dialogService.open(EditProveedorComponent, {
      header: 'Editar',
      styleClass: 'modal-catalogo',
      data: proveedor
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalEditarAbierto = false;
        this.cargarProveedores(this.proveedores?.pageSize, this.proveedores?.pageNumber, this.getNombre());
      }
    });
  }

  private getNombre() {
    return this.nombre || undefined;
  }

  filtrar() {
    this.cargarProveedores(this.proveedores?.pageSize, this.proveedores?.pageNumber, this.getNombre());
  }

}
