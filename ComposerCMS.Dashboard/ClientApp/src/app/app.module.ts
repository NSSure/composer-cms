import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { MainComponent } from './main.component';
import { LoginComponent } from './login/login.component';
import { SiteSettingsComponent } from './settings/site-settings.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ResourcesComponent } from './resources/resources.component';
import { FileUploadComponent } from './file/upload/file-upload.component';
import { ResourceListComponent } from './resources/resource-list.component';
import { HtmlEditorComponent } from './html-editor/html-editor.component';
import { ActivityHistoryComponent } from './activity-history/activity-history.component';
import { DiffComponent } from './diff/diff.component';
import { PageListComponent } from './structure/page/page-list.component';
import { PageManageComponent } from './structure/page/page-manage.component';
import { LayoutListComponent } from './structure/layout-list.component';
import { LayoutManageComponent } from './structure/layout-manage.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { PageVersionListComponent } from './structure/page/version/page-version-list.component';
import { MenuListComponent } from './structure/menu/menu-list.component';
import { MenuManageComponent } from './structure/menu/menu-manage-component';
import { BlogListComponent } from './blog/blog-list.component';
import { BlogManageComponent } from './blog/blog-manage.component';
import { PostManageComponent } from './post/post-manage.component';
import { PostListComponent } from './post/post-list.component';
import { ActiveStateIndicatorComponent } from './helpers/active-state-indicator.component';
import { MenuItemListComponent } from './structure/menu/menu-item-list.component';


const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', component: MainComponent, children: [
      { path: '', component: DashboardComponent },
      { path: 'login', component: LoginComponent },
      { path: 'layouts', component: LayoutListComponent },
      { path: 'layout', component: LayoutManageComponent },
      { path: 'pages', component: PageListComponent },
      { path: 'page', component: PageManageComponent },
      { path: 'page/:id', component: PageManageComponent },

      { path: 'menus', component: MenuListComponent },
      { path: 'menu', component: MenuManageComponent },
      { path: 'menu/:menuId', component: MenuManageComponent },

      { path: 'resources', component: ResourcesComponent },
      { path: 'activity', component: ActivityHistoryComponent },
      { path: 'settings', component: SiteSettingsComponent },
      { path: 'diff/:pageId/:pageVersionId', component: DiffComponent },
      { path: 'blogs', component: BlogListComponent },
      { path: 'blog', component: BlogManageComponent },
      { path: 'blog/:blogId', component: BlogManageComponent },
      { path: 'blog/:blogId/post', component: PostManageComponent },
      { path: 'blog/:blogId/post/:postId', component: PostManageComponent },
    ]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    LoginComponent,
    SideMenuComponent,
    PageListComponent,
    PageManageComponent,
    PageVersionListComponent,
    ActivityHistoryComponent,
    SiteSettingsComponent,
    DashboardComponent,
    LayoutListComponent,
    LayoutManageComponent,
    MenuListComponent,
    MenuManageComponent,
    MenuItemListComponent,
    ResourcesComponent,
    ResourceListComponent,
    FileUploadComponent,
    HtmlEditorComponent,
    DiffComponent,
    BlogListComponent,
    BlogManageComponent,
    PostListComponent,
    PostManageComponent,
    ActiveStateIndicatorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
