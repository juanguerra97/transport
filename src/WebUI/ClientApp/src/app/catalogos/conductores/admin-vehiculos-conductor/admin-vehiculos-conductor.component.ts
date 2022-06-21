import { Component, OnInit } from '@angular/core';
import {ConductorDto, VehiculoConductorClient, VehiculoDto} from "../../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-admin-vehiculos-conductor',
  templateUrl: './admin-vehiculos-conductor.component.html',
  styleUrls: ['./admin-vehiculos-conductor.component.scss']
})
export class AdminVehiculosConductorComponent implements OnInit {

  form = new FormGroup({
    conductorId: new FormControl(null, [Validators.required]),
    vehiculoId: new FormControl(null, [Validators.required])
  });

  conductor?: ConductorDto;
  guardando = false;

  vehiculos: VehiculoDto[] = [];
  vehiculosDisponibles: VehiculoDto[] = [];

  constructor(
    private vehiculoConductorClient: VehiculoConductorClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) { }

  get conductorId() {
    return this.form.get('conductorId');
  }

  get vehiculoId() {
    return this.form.get('vehiculoId');
  }

  ngOnInit(): void {
    this.conductor = this.config.data;
    this.conductorId?.patchValue(this.conductor?.id);
    this.cargarVehiculos(this.conductor?.id);
    this.cargarVehiculosDisponibles(this.conductor?.id);
  }

  cargarVehiculos(conductorId: any) {
    this.vehiculoConductorClient.getVehiculosByConductor(conductorId)
      .subscribe({
        next: res => {
          this.vehiculos = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 5000,
            position: 'bottom-center'
          });
        }
      });
  }

  cargarVehiculosDisponibles(conductorId: any) {
    this.vehiculoConductorClient.getVehiculosDisponiblesByConductor(conductorId)
      .subscribe({
        next: res => {
          this.vehiculosDisponibles = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 5000,
            position: 'bottom-center'
          });
        }
      });
  }

  agregar() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.guardando = true;
    this.vehiculoConductorClient.create(this.form.value)
      .subscribe({
        next: res => {
          this.cargarVehiculos(this.conductor?.id);
          this.cargarVehiculosDisponibles(this.conductor?.id);
          this.vehiculoId?.reset();
          this.guardando = false;
          this.toastService.success('Se agrego un vehiculo al conductor.', {
            dismissible: true,
            duration: 3000,
            position: 'bottom-center'
          });
        },
        error: error => {
          console.error(error);
          this.guardando = false;
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 10000,
            position: 'bottom-center'
          });
        }
      });
  }

  eliminar(vehiculo: VehiculoDto) {
    const val: any = {
      conductorId: this.conductor?.id,
      vehiculoId: vehiculo.id
    };
    this.vehiculoConductorClient.delete(val)
      .subscribe({
        next: res => {
          this.cargarVehiculos(this.conductor?.id);
          this.cargarVehiculosDisponibles(this.conductor?.id);
          this.toastService.success('Se elimino un vehiculo del conductor.', {
            dismissible: true,
            duration: 3000,
            position: 'bottom-center'
          });
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 10000,
            position: 'bottom-center'
          });
        }
      });
  }

}
