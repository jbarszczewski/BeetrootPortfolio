import { Component, OnInit } from '@angular/core';

import { ProjectService } from '../../services/project.service';

import { Project } from '../../models/project';

@Component({
  selector: 'projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {
  projects: Project[] = [];

  constructor(private projectService: ProjectService) {  }
  
    ngOnInit(): void {      
    this.projectService.GetAllProjects()
    .subscribe((res: Project[]) => this.projects = res);
    }
}