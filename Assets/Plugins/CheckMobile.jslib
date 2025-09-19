var MyPlugin = {
  IsMobile: function () {
    try {
      var ua = (typeof navigator !== 'undefined' && navigator.userAgent) ? navigator.userAgent : "";
      var isMobileUA = /Android|iPhone|iPad|iPod/i.test(ua);

      // Có thể bổ sung heuristics theo kích thước màn hình nếu muốn:
      var isSmallViewport = false;
      if (typeof window !== 'undefined' && window.innerWidth && window.innerHeight) {
        var shortSide = Math.min(window.innerWidth, window.innerHeight);
        isSmallViewport = shortSide <= 812; // ngưỡng phổ biến cho mobile
      }

      // Kết luận:
      var mobile = isMobileUA || isSmallViewport;

      // 🔑 Trả về 1/0 (number). IL2CPP/WebGL map non-zero → true, 0 → false cho extern bool.
      return mobile ? 1 : 0;
    } catch (e) {
      return 0;
    }
  }
};

mergeInto(LibraryManager.library, MyPlugin);
