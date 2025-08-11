-- VAICOM PRO server-side script
-- TabSheetBar.lua (append)
-- www.vaicompro.com

function construct(self)
    -- === UI Skin Definition ===
    self.skin = {
        container     = {},
        item          = Skin.staticSkin(),
        selectedItem  = Skin.staticSkin(),
        arrows        = Skin.staticSkin(),
        spacing       = 0
    }

    -- === Window Setup ===
    self.window = Window.new(0, 0, 1280, 30)
    self.window:setHasCursor(false)
    self.window:setVisible(false)

    -- === Container Panel ===
    self.container = Panel.new()
    self.container:setSkin(self.skin.container)
    self.window:insertWidget(self.container)

    local _, _, width, _ = self.container:getBounds()

    -- === Navigation Arrows ===
    local function createArrow()
        local arrow = Static.new('...')
        arrow:setSkin(self.skin.arrows)
        local w, h = arrow:calcSize()
        arrow:setBounds(0, 0, w, h)
        arrow:setVisible(false)
        return arrow, w, h
    end

    local left, _, _ = createArrow()
    self.container:insertWidget(left)
    self.left = left

    local right, rightW, rightH = createArrow()
    right:setBounds(width - rightW, 0, rightW, rightH)
    self.container:insertWidget(right)
    self.right = right

    -- === Tab State Initialization ===
    self.currentTabIndex = 0
    self.firstTabIndex   = 0
    self.lastTabIndex    = 0
    self.tabsWidth       = 0
    self.tabs            = {}
end
