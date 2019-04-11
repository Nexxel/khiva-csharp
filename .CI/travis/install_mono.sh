#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.

travis_retry nuget restore -source "https://www.nuget.org/api/v2" KhivaCsharp/KhivaCsharp.sln