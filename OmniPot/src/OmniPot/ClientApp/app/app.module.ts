import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";
import "rxjs/Rx";

import { AppComponent } from "./components/app.component";
import { LoginComponent } from "./components/login/login.component";

import { AppRouting } from "./app.routing";
import { AppService } from "./services/app.service";

@NgModule({
    // modules
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,       
        RouterModule,
        AppRouting
    ],
    // directives, components, and pipes
    declarations: [
        AppComponent,
        LoginComponent
    ],    
    // providers
    providers: [
        AppService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }  