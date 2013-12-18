﻿// Copyright © 2013 Open Octopus Ltd.
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
// 
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using OpenCBS.DataContract;
using OpenCBS.DataContract.AppEvent;
using OpenCBS.DataContract.CommandData;
using OpenCBS.Interface;
using OpenCBS.Interface.Presenter;
using OpenCBS.Interface.Service;

namespace OpenCBS.Service.Command
{
    public class EditUserCommand : ICommand<EditUserData>
    {
        private readonly IUserPresenter _presenter;
        private readonly IUserService _userService;
        private readonly IApplicationController _appController;

        public EditUserCommand(IUserPresenter presenter, IUserService userService, IApplicationController appController)
        {
            _presenter = presenter;
            _userService = userService;
            _appController = appController;
        }

        public void Execute(EditUserData commandData)
        {
            var userDto = _userService.FindById(commandData.Id);
            var result = _presenter.Get(userDto);
            if (result.CommandResult != CommandResult.Ok) return;

            _userService.Update(result.Data);
            _appController.Raise(new UserSavedEvent { Id = result.Data.Id });
        }
    }
}