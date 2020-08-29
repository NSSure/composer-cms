export class Post {
  id: string;
  blogId: string;
  title: string;
  description: string;
  content: string;
  isPinned: boolean;
  isPublished: boolean;
  isPublic: boolean;
  dateAdded: Date;
  dateLastUpdated: Date;
}
