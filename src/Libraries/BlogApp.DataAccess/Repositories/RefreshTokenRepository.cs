﻿using BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Contexts;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApp.DataAccess.Repositories;
public class RefreshTokenRepository : EfBaseRepository<RefreshToken, BlogAppDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BlogAppDbContext context, ILogger<RefreshTokenRepository> logger) : base(context, logger) { }

    public async Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            return await _table.Where(x => x.Token == refreshToken)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateRefreshTokenAsUsed(RefreshToken refreshToken)
    {
        try
        {
            var token = await _table.Where(x => x.Token == refreshToken.Token)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

            if (token == null) return false;

            token.RevokedDate = DateTime.Now;

            _ = await UpdateAsync(token);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);
            return false;
        }
    }
}
