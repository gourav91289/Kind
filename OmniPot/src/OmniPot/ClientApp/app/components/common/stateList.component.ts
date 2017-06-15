import { Component, NgModule} from '@angular/core'
import {BrowserModule} from '@angular/platform-browser'
import { DropDownList } from '../../viewmodels/common/DropDownList';

@Component({
    selector: 'statelist',
    template: `<select class="form-control">
                    <option *ngFor="let country of countries" value={{country.id}}>
                        {{country.name}}
                    </option>
                </select>`
})
export class StateListComponent {
    selectedCountry: DropDownList = new DropDownList(2, 'India');
  countries = [
     new DropDownList(1, 'USA' ),
     new DropDownList(2, 'India' ),
     new DropDownList(3, 'Australia' ),
     new DropDownList(4, 'Brazil')
  ];
}