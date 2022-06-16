import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ProveedorDto} from "../../../web-api-client";

@Component({
  selector: 'app-form-proveedor',
  templateUrl: './form-proveedor.component.html',
  styleUrls: ['./form-proveedor.component.scss']
})
export class FormProveedorComponent implements OnInit {

  _proveedor?: ProveedorDto;

  @Input() set proveedor(value: ProveedorDto|undefined) {
    this._proveedor = value;
    if (value) {
      this.form.patchValue(value);
    } else {
      this.form.reset();
    }
  }

  @Input() guardando = false;

  @Output() onGuardar = new EventEmitter<ProveedorDto>();
  @Output() onCancelar = new EventEmitter<null>();

  form = new FormGroup({
    nombre: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    nit: new FormControl(null, [Validators.required, Validators.maxLength(32)]),
    telefono: new FormControl(null, [Validators.required, Validators.minLength(8), Validators.maxLength(8)]),
    email: new FormControl(null, [Validators.required, Validators.email, Validators.maxLength(256)]),
    direccion: new FormControl(null, [Validators.required, Validators.maxLength(256)])
  });

  constructor() { }

  get nombre() {
    return this.form.get('nombre');
  }

  get nit() {
    return this.form.get('nit');
  }

  get telefono() {
    return this.form.get('telefono');
  }

  get email() {
    return this.form.get('email');
  }

  get direccion() {
    return this.form.get('direccion');
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
