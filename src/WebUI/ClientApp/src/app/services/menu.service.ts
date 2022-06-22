import {Injectable} from '@angular/core';
import {AuthorizeService, Roles} from "../../api-authorization/authorize.service";
import {BehaviorSubject} from "rxjs";
import {INavData} from "@coreui/angular";

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  private _menuSubject = new BehaviorSubject<INavData[]>([]);
  public menuData = this._menuSubject.asObservable();

  constructor(
    private authorizeService: AuthorizeService,
  ) {
    this.authorizeService.getUser().subscribe({
      next: user => {
        if (!user) {
          this._menuSubject.next([]);
        } else {
          const menu: INavData[] = [];

          if (user.role?.includes(Roles.ADMINISTRATOR)) {
            menu.push({
                title: true,
                name: 'Administración'
              },
              {
                name: 'Catálogos',
                icon: 'fa fa-solid fa-table',
                children: [
                  {name: 'Plantas', url: '/admin/catalogo/plantas'},
                  {name: 'Bodegas', url: '/admin/catalogo/bodegas'},
                  {name: 'Materiales', url: '/admin/catalogo/materiales'},
                  {name: 'Medidas', url: '/admin/catalogo/medidas'},
                  {name: 'Proveedores', url: '/admin/catalogo/proveedores'},
                  {name: 'Vehiculos', url: '/admin/catalogo/vehiculos'},
                  {name: 'Conductores', url: '/admin/catalogo/conductores'}
                ]
              });
          }
          if (user.role?.includes(Roles.ADMIN_BODEGA) || user.role?.includes(Roles.ADMIN_PEDIDOS)) {
            menu.push({
              title: true,
              name: 'Inventario'
            });
            if (user.role?.includes(Roles.ADMIN_BODEGA)) {
              menu.push({
                name: 'Bodegas',
                icon: 'fa fa-solid fa-warehouse',
                url: '/inventario'
              });
            }
            if (user.role?.includes(Roles.ADMIN_PEDIDOS)) {
              menu.push({
                name: 'Pedidos',
                icon: 'fa-solid fa-cart-shopping',
                url: '/pedidos'
              });
            }
          }
          if (user.role?.includes(Roles.CONDUCTOR)) {
            menu.push({
              title: true,
              name: 'Conductores'
            });
            menu.push({
              name: 'Entregas',
              icon: 'fa-solid fa-truck',
              url: '/entregas'
            });
          }
          this._menuSubject.next(menu);
        }
      }
    });
  }
}
