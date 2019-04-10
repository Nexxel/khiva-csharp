#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.

ls Library/Frameworks/Mono.framework
msbuild /p:Configuration=Debug KhivaCsharp/KhivaCsharp.sln
mono ./testrunner/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./KhivaCsharp/KhivaCSharpNUnitTest/bin/Debug/KhivaCSharpNUnitTest.dll