import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VehiculoDto} from "../../../web-api-client";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-form-vehiculo',
  templateUrl: './form-vehiculo.component.html',
  styleUrls: ['./form-vehiculo.component.scss']
})
export class FormVehiculoComponent implements OnInit {

  _vehiculo?: VehiculoDto;

  @Input() set vehiculo(value: VehiculoDto|undefined) {
    this._vehiculo = value;
    if (value) {
      this.form.patchValue(value);
    } else {
      this.form.reset();
    }
  }

  @Input() guardando = false;

  @Output() onGuardar = new EventEmitter<VehiculoDto>();
  @Output() onCancelar = new EventEmitter<null>();

  form = new FormGroup({
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    detalle: new FormControl(null, [Validators.required, Validators.maxLength(1024)]),
    codigo: new FormControl(null, [Validators.required, Validators.maxLength(128)]),
    placa: new FormControl(null, [Validators.required, Validators.maxLength(64)]),
    capacidadCarga: new FormControl(null, [Validators.required]),
  });

  constructor() { }

  get descripcion() {
    return this.form.get('descripcion');
  }

  get detalle() {
    return this.form.get('detalle');
  }

  get codigo() {
    return this.form.get('codigo');
  }

  get placa() {
    return this.form.get('placa');
  }

  get capacidadCarga() {
    return this.form.get('capacidadCarga');
  }

  ngOnInit(): void {
  }

  guardar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.onGuardar.emit(this.form.value);
  }

  cancelar() {
    this.onCancelar.emit(null);
  }

}
