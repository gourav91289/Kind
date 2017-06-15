System.register(["@angular/core", "../../viewmodels/common/DropDownList"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, DropDownList_1, CountryListComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (DropDownList_1_1) {
                DropDownList_1 = DropDownList_1_1;
            }
        ],
        execute: function () {
            CountryListComponent = (function () {
                //import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
                function CountryListComponent() {
                    this.selectedCountry = new DropDownList_1.DropDownList(2, 'India');
                    this.countries = [
                        new DropDownList_1.DropDownList(1, 'USA'),
                        new DropDownList_1.DropDownList(2, 'India'),
                        new DropDownList_1.DropDownList(3, 'Australia'),
                        new DropDownList_1.DropDownList(4, 'Brazil')
                    ];
                }
                return CountryListComponent;
            }());
            CountryListComponent = __decorate([
                core_1.Component({
                    selector: 'countrylist',
                    template: "<select class=\"form-control\">\n                    <option *ngFor=\"let country of countries\" value={{country.id}}>\n                        {{country.name}}\n                    </option>\n                </select>"
                })
            ], CountryListComponent);
            exports_1("CountryListComponent", CountryListComponent);
        }
    };
});
//# sourceMappingURL=countryList.component.js.map