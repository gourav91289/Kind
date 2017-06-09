//<reference path="../../typings/index.d.ts"/>
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import "rxjs/Rx";

import { AppComponent } from "./components/app.component";
import { LoginComponent } from "./components/login/login.component";

import { AppRouting } from "./app.routing";
import { AppService } from "./services/app.service";

@NgModule({
    // directives, components, and pipes
    declarations: [
        AppComponent,
        LoginComponent
    ],
    // modules
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting
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