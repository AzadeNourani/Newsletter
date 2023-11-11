using Microsoft.EntityFrameworkCore.Query;
using NewsletterAPI.Data.Contexts;
using NewsletterAPI.Data.Models;
using NewsletterAPI.Data.Services.Queries.GetLastNews;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace APIUnitTest
{
    public class GetLastNewsServiceTests
    {
        [Fact]
        public async Task GetLastNewsletterAsync_ReturnsLastNewsletter()
        {
            // Arrange
            var dbContextMock = new Mock<NewsDbContext>();
            var lastNewsletter = new Newsletter { Id = 1, SendDate = DateTime.UtcNow };
            dbContextMock.Setup(db => db.Newsletter)
                .Returns(new DbSetMock<Newsletter>().SetupAsyncQuery(lastNewsletter));

            var getNewsService = new GetLastNewsService(dbContextMock.Object);

            // Act
            var result = await getNewsService.GetLastNewsletterAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(lastNewsletter, result);
        }

        [Fact]
        public async Task GetLastNewsletterAsync_ReturnsDefaultNewsletterOnNull()
        {
            // Arrange
            var dbContextMock = new Mock<NewsDbContext>();
            dbContextMock.Setup(db => db.Newsletter)
                .Returns(new DbSetMock<Newsletter>().SetupAsyncQuery(null));

            var getNewsService = new GetLastNewsService(dbContextMock.Object);

            // Act
            var result = await getNewsService.GetLastNewsletterAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id); // Assuming default Id is 1
        }
    }

    public class DbSetMock<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
    {
        public DbSetMock<TEntity> SetupAsyncQuery(TEntity result)
        {
            var data = new TestDataQueryable<TEntity>(result);
            this.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            this.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            this.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            this.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            return this;
        }
    }

    public class TestDataQueryable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDataQueryable(IEnumerable<T> enumerable) : base(enumerable)
        { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator().AsAsyncEnumerator());
        }

        public IEnumerator<T> GetEnumerator() => this.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Type ElementType => typeof(T);
        public Expression Expression => Expression.Constant(this);
        public IQueryProvider Provider => new AsyncQueryProvider<T>(this);
    }

    // The rest of your test utility classes remain unchanged.

    //public class GetLastNewsServiceTests
    //{
    //    [Fact]
    //    public async Task GetLastNewsletterAsync_ReturnsLastNewsletter()
    //    {
    //        // Arrange
    //        var dbContextMock = new Mock<NewsDbContext>();
    //        var lastNewsletter = new Newsletter { Id = 1, SendDate = DateTime.UtcNow };
    //        var dbSetMock = new DbSetMock<Newsletter>();
    //        dbSetMock.SetupAsyncQuery(lastNewsletter);
    //        dbContextMock.Setup(db => db.Newsletter)
    //            .Returns(new DbSetMock<Newsletter>().SetupAsyncQuery(lastNewsletter));

    //        var getNewsService = new GetLastNewsService(dbContextMock.Object);

    //        // Act
    //        var result = await getNewsService.GetLastNewsletterAsync();

    //        // Assert
    //        Assert.NotNull(result);
    //        Assert.Equal(lastNewsletter, result);
    //    }

    //    [Fact]
    //    public async Task GetLastNewsletterAsync_ReturnsDefaultNewsletterOnNull()
    //    {
    //        // Arrange
    //        var dbContextMock = new Mock<NewsDbContext>();
    //        dbContextMock.Setup(db => db.Newsletter)
    //            .Returns(new DbSetMock<Newsletter>().SetupAsyncQuery(null));

    //        var getNewsService = new GetLastNewsService(dbContextMock.Object);

    //        // Act
    //        var result = await getNewsService.GetLastNewsletterAsync();

    //        // Assert
    //        Assert.NotNull(result);
    //        Assert.Equal(1, result.Id); // Assuming default Id is 1
    //    }
    //}

    //// Mock for DbSet to simulate async queries
    //public class DbSetMock<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
    //{
    //    public DbSetMock<TEntity> SetupAsyncQuery(TEntity result)
    //    {
    //        var data = new TestDataQueryable<TEntity>(result);
    //        this.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
    //        this.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
    //        this.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //        this.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
    //        return this;
    //    }
    //}

    //// Test data IQueryable implementation
    //public class TestDataQueryable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    //{
    //    public TestDataQueryable(IEnumerable<T> enumerable) : base(enumerable)
    //    { }

    //    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    //    //IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
    //    public IEnumerator<T> GetEnumerator() => this.AsEnumerable().GetEnumerator();
    //    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    //    public Type ElementType => typeof(T);
    //    public Expression Expression => Expression.Constant(this);

    //    public IQueryProvider Provider => new AsyncQueryProvider<T>(this);
    //}

    //// AsyncEnumerator and AsyncQueryProvider for async testing
    //public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    //{
    //    private readonly IEnumerator<T> _inner;

    //    public AsyncEnumerator(IEnumerator<T> inner) => _inner = inner;

    //    public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());

    //    public T Current => _inner.Current;

    //    public ValueTask DisposeAsync() => new ValueTask(Task.CompletedTask);
    //}

    //public class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
    //{
    //    private readonly IQueryProvider _inner;

    //    public AsyncQueryProvider(IQueryProvider inner) => _inner = inner;

    //    public IQueryable CreateQuery(Expression expression) => new TestDataQueryable<TEntity>(_inner.CreateQuery<TEntity>(expression));

    //    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestDataQueryable<TElement>(_inner.CreateQuery<TElement>(expression));


    //    public object Execute(Expression expression) => _inner.Execute(expression);

    //    public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);


    //    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression) => new TestAsyncEnumerable(expression);

    //    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    //{
    //    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
    //    { }

    //    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    //    IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
    //}

}