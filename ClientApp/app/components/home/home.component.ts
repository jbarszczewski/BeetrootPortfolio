import { Component, OnInit } from '@angular/core';

import { ProjectService } from '../../services/project.service';

import { Info } from '../../models/info';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent {
    bio: string = "Gathering personal data...";
    isLoading: boolean = true;

    constructor(private projectService: ProjectService) { }

    ngOnInit(): void {
        this.projectService.GetInfo("bio")
            .subscribe((res: Info) => {
                this.bio = res.value;
            }, (err: any) => { } , () => this.isLoading = false);
    }
}
