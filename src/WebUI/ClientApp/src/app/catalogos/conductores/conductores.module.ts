import {NgModule} from '@angular/core';
import {CoreModule} from "../../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import {HomeConductoresComponent} from './home-conductores/home-conductores.component';
import {EditConductorComponent} from './edit-conductor/edit-conductor.component';
import {NewConductorComponent} from './new-conductor/new-conductor.component';
import {FormConductorComponent} from './form-conductor/form-conductor.component';
import { AdminVehiculosConductorComponent } from './admin-vehiculos-conductor/admin-vehiculos-conductor.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeConductoresComponent},
];

@NgModule({
  declarations: [
    HomeConductoresComponent,
    EditConductorComponent,
    NewConductorComponent,
    FormConductorComponent,
    AdminVehiculosConductorComponent,
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class ConductoresModule {
}
