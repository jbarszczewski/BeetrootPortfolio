import { Component } from '@angular/core';

import { ProjectService } from '../../services/project.service';

import { Project } from '../../models/project';

@Component({
  selector: 'project',
  templateUrl: './project.component.html'
})

export class ProjectComponent {
  title: string = "Project:";
  project: Project;
  constructor(private projectService: ProjectService) {
    this.projectService.GetProject(1)
      .subscribe(
      (res: Project) => {
        this.project = res;
      });
  }


}