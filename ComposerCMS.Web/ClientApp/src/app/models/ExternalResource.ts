export class ExternalResource {
  id: string;
  externalResourcePackageId: string;
  name: string;
  extension: string;
  path: string;
  order: string;
  dateAdded: Date;
  dateLastUpdated: Date;
  userIdAdded: string;
  userIdUpdated: string;

  // TODO: Convert this to a model.
  userName: string;
}
