import { NgModule } from '@angular/core';
import {GridModule, HeaderModule, NavModule, SidebarModule} from '@coreui/angular';
import {PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule} from "ngx-perfect-scrollbar";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
};

@NgModule({
  declarations: [],
  imports: [
    PerfectScrollbarModule
  ],
  exports: [
    SidebarModule,
    HeaderModule,
    NavModule,
    GridModule,
    PerfectScrollbarModule
  ],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG,
    },
  ]
})
export class CoreUiModule { }
