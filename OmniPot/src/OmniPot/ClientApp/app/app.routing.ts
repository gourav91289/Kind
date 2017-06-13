import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from './security/auth-guard.service';

import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/login/registration.component";
//import { RegistrationComponent } from "./components/admin/users/registration.component";

const routes: Routes = [
    {  path: '', redirectTo: '',  pathMatch: 'full'  },
    { path: 'login', component: LoginComponent, data: { title: 'Login' } },
    { path: 'registration', component: RegistrationComponent, data: { title: 'Registration' } },
  //{ path: 'dashboard', component:DashboardComponent, data: { title: 'User Dashboard' }, canActivate: [AuthGuard] },
  //{ path: 'explore/:id',component: PlaceDetailComponent }

];


export const AppRoutingProviders: any[] = [];
export const AppRouting: ModuleWithProviders = RouterModule.forRoot(routes);  