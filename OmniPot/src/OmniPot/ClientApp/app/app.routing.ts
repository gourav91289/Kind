import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from './security/auth-guard.service';

import { LoginComponent } from "./components/login/login.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { TokenVerifyComponent } from "./components/token.verify.component";


const routes: Routes = [
    // Verifying token
    { path: '', component: TokenVerifyComponent, canActivate: [AuthGuard] },

    // App views
    { path: 'login', component: LoginComponent },
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },


    //{ path: '', redirectTo: '', pathMatch: 'full' },    
  //{ path: 'dashboard', component:DashboardComponent, data: { title: 'User Dashboard' }, canActivate: [AuthGuard] },
  //{ path: 'explore/:id',component: PlaceDetailComponent }

];

export const AppRoutingProviders: any[] = [];
export const AppRouting: ModuleWithProviders = RouterModule.forRoot(routes);  