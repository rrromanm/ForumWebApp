﻿using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting forum app...");

IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

CliApp cliApp = new CliApp(userRepository, postRepository, commentRepository );
await cliApp.StartAsync();


