--[[ VAICOM BEGIN EXPORT BLOCK ]]
-- Load VAICOMPRO Export Script Safely
local lfs_status, lfs = pcall(require, 'lfs')
if lfs_status and lfs and type(lfs.writedir) == "function" then
    local exportPath = lfs.writedir() .. [[Scripts\VAICOMPRO\VAICOMPRO.export.lua]]
    local export_status, err = pcall(dofile, exportPath)
    if not export_status then
        log.write("VAICOM", log.ERROR, "Failed to execute VAICOMPRO.export.lua: " .. tostring(err))
    end
else
    log.write("VAICOM", log.ERROR, "Failed to load LuaFileSystem or invalid lfs.writedir()")
end
--[[ VAICOM END EXPORT BLOCK ]]