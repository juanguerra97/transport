import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ConductorDto, UsuarioClient, UsuarioDto} from "../../../web-api-client";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {getErrorMessage} from "../../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";

@Component({
  selector: 'app-form-conductor',
  templateUrl: './form-conductor.component.html',
  styleUrls: ['./form-conductor.component.scss']
})
export class FormConductorComponent implements OnInit {

  _conductor?: ConductorDto;

  @Input() set conductor(value: ConductorDto | undefined) {
    this._conductor = value;
    this.form.enable();
    if (value) {
      this.form.patchValue(value);
      this.user?.disable();
    } else {
      this.form.reset();
    }
  }

  @Input() guardando = false;

  @Output() onGuardar = new EventEmitter<ConductorDto>();
  @Output() onCancelar = new EventEmitter<null>();

  form = new FormGroup({
    user: new FormControl(null, [Validators.required]),
    noLicencia: new FormControl(null, [Validators.required, Validators.maxLength(64)])
  });

  suggestionsUsuario: UsuarioDto[] = [];

  constructor(
    private usuarioClient: UsuarioClient,
    private toastService: HotToastService,
  ) {
  }

  get user() {
    return this.form.get('user');
  }

  get noLicencia() {
    return this.form.get('noLicencia');
  }

  ngOnInit(): void {
  }

  guardar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const val = Object.assign({}, this.form.value);
    val.userId = val.user?.id;
    delete val.user;
    this.onGuardar.emit(val);
  }

  cancelar() {
    this.onCancelar.emit(null);
  }

  searchUsuario($event: any) {
    this.usuarioClient.searchUsuariosByName($event.query, 5)
      .subscribe({
        next: res => {
          this.suggestionsUsuario = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            autoClose: false,
            dismissible: true,
            position: "bottom-center"
          });
        }
      });
  }

  getUsuario(usuario: UsuarioDto) {
    return `${usuario.firstName} ${usuario.lastName} (${usuario.userName})`;
  }

}
