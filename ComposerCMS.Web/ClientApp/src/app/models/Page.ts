export class Page {
  id: string;
  name: string;
  title: string;
  content: string = '';
  path: string;
  isSystemPage: boolean;
  isAbstract: boolean;
  isPublished: boolean;
  sourceVersionId: string;
  dateAdded: Date;
  dateLastUpdated: Date;
  dateLastPublished: Date;
}
