import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { ProjectService } from '../../services/project.service';

import { Project } from '../../models/project';

import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'project',
  templateUrl: './project.component.html'
})

export class ProjectComponent implements OnInit {
  project: Project = new Project();
  constructor(private projectService: ProjectService, private route: ActivatedRoute) {  }

  ngOnInit(): void {
    this.route.params
    .switchMap((params: Params) => this.projectService.GetProject(params['id']))
      .subscribe(
      (res: Project) => {
        this.project = res;
      });
  }
}