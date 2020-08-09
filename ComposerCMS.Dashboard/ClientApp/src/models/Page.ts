export class Page {
  id: string;
  name: string;
  title: string;
  content: string = '';
  path: string;
  sourceVersionId: string;
  dateAdded: Date;
  dateLastUpdated: Date;
  dateLastPublished: Date;
}
