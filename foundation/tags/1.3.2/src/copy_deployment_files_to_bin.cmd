xcopy deploy\Debug ..\binaries\net3.5\debug /U /Y /S
xcopy deploy\Debug-silver ..\binaries\silver3.0\debug /U /Y /S
xcopy deploy\Release ..\binaries\net3.5\release /U /Y /S
xcopy deploy\Release-silver ..\binaries\silver3.0\release /U /Y /S
pause