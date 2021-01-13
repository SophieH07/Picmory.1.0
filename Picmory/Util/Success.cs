﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Util
{
    public enum Success
    {
        Successfull,
        FailedByUsedName,
        FailedByNotExist,
        FailedByNotExistFolderName,
        FailedByAlreadyRequested,
        FailedByAlreadyFollowed,
        FailedByNotRequested,
        FailedByAlreadyAnswered,
        FailedByNotAccepted,
        FailedByWrongAccessNewFolder,
        FailedByWrongAccessFolder

    }
}