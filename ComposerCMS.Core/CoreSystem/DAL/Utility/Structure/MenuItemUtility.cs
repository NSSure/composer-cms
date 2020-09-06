using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class MenuItemUtility : BaseRepository<MenuItem>
    {
        public MenuItemUtility(UserResolver userResolver) : base(userResolver)
        {

        }

        public async Task IncrementOrder(Guid menuID, Guid menuItemID)
        {
            List<MenuItem> _pendingOrderReset = await this.Table.Where(a => a.MenuID == menuID).OrderBy(a => a.Order).ToListAsync();

            MenuItem _targetMenuItem = _pendingOrderReset.FirstOrDefault(a => a.ID == menuItemID);

            int _targetIndex = _pendingOrderReset.IndexOf(_targetMenuItem);

            MenuItem _previousHigherItem = _pendingOrderReset[_targetIndex + 1];
            _previousHigherItem.Order--;
            _targetMenuItem.Order++;

            await this.UpdateAsync(_targetMenuItem);
            await this.UpdateAsync(_previousHigherItem);
        }

        public async Task DecrementOrder(Guid menuID, Guid menuItemID)
        {
            List<MenuItem> _pendingOrderReset = await this.Table.Where(a => a.MenuID == menuID).OrderByDescending(a => a.Order).ToListAsync();

            MenuItem _targetMenuItem = _pendingOrderReset.FirstOrDefault(a => a.ID == menuItemID);

            int _targetIndex = _pendingOrderReset.IndexOf(_targetMenuItem);

            MenuItem _previousLowerItem = _pendingOrderReset[_targetIndex + 1];
            _previousLowerItem.Order++;
            _targetMenuItem.Order--;

            await this.UpdateAsync(_targetMenuItem);
            await this.UpdateAsync(_previousLowerItem);
        }
    }
}