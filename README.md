# PlandeyCodeChallange

Regarding the tasks, I really enjoyed working on it.

The first task was fairly easy, and I was able to complete it within 20 minutes.
The second task was also manageable; I spent most of my time on validation to provide the cleanest solution I could. 
The third task was similar, but I invested most of my time in handling informational errors.
The fourth task was a bit more challenging, but I managed to finish it within 40 minutes. The last 30 minutes were spent figuring out how the authentication works, with a bit of help from PostmanAPI.

Afterward, I cleaned up the solution and refactored/optimized a few things that I wasn't entirely satisfied with. In total, I was able to complete everything within three and a half hours.

I also began working on the tests. I started with FakeItEasy, but then I realized that the QueryAsync function is static, so I had to switch back to using Moq. The most challenging part was mocking the SQLite database, which I unfortunately failed to do, and my time was running out. So, after four hours, I decided to stop because I wanted to showcase what I can achieve within the given time frame. In my free time, I plan to explore this further, as it's frustrating to be so close to the solution. For the other test cases, I would have encountered the same solution with different input fields. And do some validator tests as well.

Ideas to improve the solution:

  - I prefer using MediatR to handle commands/queries and their respective handlers.
  - Instead of creating a class with constants, it's better to place them in the appsettings file. This way, the application       can be configured to work in production without any code changes, such as adjusting URIs in my solution.
  - Other than these points, I couldn't find anything substantial to improve, given the size of the application.
