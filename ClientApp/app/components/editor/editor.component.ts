﻿import { Component } from '@angular/core';

import { ProjectService } from '../../services/project.service';

import { Project } from '../../models/project';

@Component({
    selector: 'editor',
    templateUrl: './editor.component.html'
})
export class EditorComponent {
    project: Project = new Project();
    apiKey: string;

    get diagnostic() { return JSON.stringify(this.project); }

    constructor(private projectService: ProjectService) { }    
    
    onSubmit() {
        this.project.createdOn = Date.now();
        this.projectService.SaveProject(this.apiKey, this.project).subscribe((res: Project) => this.project = res);
    }
}