#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.

PATH=/usr/local/bin:$PATH
mono --runtime=v4.0 /usr/local/bin/NuGet.exe restore KhivaCsharp/KhivaCsharp.sln
pip install codecov