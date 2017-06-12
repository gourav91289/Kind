import { Component, OnInit } from "@angular/core";

@Component({
    selector: "dashboard",
    template: `<div class="container">
    <div class="col-lg-5 col-sm-6 col-lg-offset-1 child">
        <img [src]="logoPath" style="margin:40% 0 0 0" class="img-responsive" alt="AGRSoft">
    </div>`,
})

export class DashboardComponent implements OnInit {
    logoPath: string;

    constructor() {
        this.logoPath = './images/logo.png';       
    }

    ngOnInit() {        
    }

}