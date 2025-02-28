export interface Blog{
    id: string;
    title: string;
    description: string;
    status: string;
    createdAt?: Date;
    updatedAt?: Date;
    blogImage?: BlogImage[];
}

export interface BlogImage{
    id: string;
    blogId: string;
    imageUrl: string;
    createdAt?: Date;
    updatedAt?: Date;
}