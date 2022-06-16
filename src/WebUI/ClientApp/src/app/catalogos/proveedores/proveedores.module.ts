import {NgModule} from '@angular/core';
import {CoreModule} from "../../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import { HomeProveedoresComponent } from './home-proveedores/home-proveedores.component';
import { FormProveedorComponent } from './form-proveedor/form-proveedor.component';
import { NewProveedorComponent } from './new-proveedor/new-proveedor.component';
import { EditProveedorComponent } from './edit-proveedor/edit-proveedor.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeProveedoresComponent},
];

@NgModule({
  declarations: [
    HomeProveedoresComponent,
    FormProveedorComponent,
    NewProveedorComponent,
    EditProveedorComponent,
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes),
  ]
})
export class ProveedoresModule {
}
