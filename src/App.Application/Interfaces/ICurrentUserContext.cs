﻿using App.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces;

public interface ICurrentUserService
{
    public string UserId { get; }
    public UserRole UserRole { get; }
}
